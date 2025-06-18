using ClinicManagementSystem.Helpers;
using Domain.Contracts.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Policy;
using System.Text.Json;

namespace Service.AuthenticationService
{
	public class AuthenticationService(UserManager<User> userManager, IOptions<JwtOptions> options, HttpClient _httpClient) : IAuthenticationService
	{

		public async Task DeleteAsync(string email)
		{
			var user = await userManager.FindByEmailAsync(email);

			//  if (user == null)
			//       throw new NotFoundException("User not found");

			var result = await userManager.DeleteAsync(user);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				throw new ValidationException(errors);
			}
		}

		public async Task<UserResultDTO> LoginAsync(UserLoginDTO userLogin)
		{
			var user = await userManager.FindByEmailAsync(userLogin.Email);
			if (user == null) throw new UnAuthorizedException("Email Doesn't Exist");

			var result = await userManager.CheckPasswordAsync(user, userLogin.Password);
			if (!result) throw new UnAuthorizedException();
			return new UserResultDTO(
				user.DisplayName
				, user.Email
				, await CreateTokenAsync(user)
				, user.UserName);
		}

		public async Task<UserResultDTO> RegisterAsync(UserRegisterDTO userRegister)
		{

			var user = new User()
			{
				Email = userRegister.Email,
				DisplayName = userRegister.DisplayName,
				UserName = userRegister.DisplayName,
				PhoneNumber = userRegister.PhoneNumber,

			};

			var result = await userManager.CreateAsync(user, userRegister.Password);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();

				throw new ValidationException(errors);
			}
			await userManager.AddToRoleAsync(user, userRegister.Role);


			return new UserResultDTO(
						 user.DisplayName,
						 user.Email,
						 await CreateTokenAsync(user),
						 user.UserName
						 );

		}

		private async Task<string> CreateTokenAsync(User user)
		{
			var Jwtoptions = options.Value;
			// Private Claims
			var authclaim = new List<Claim>
			{
				new Claim(ClaimTypes.Name,user.UserName!),
				new Claim(ClaimTypes.Email,user.Email!)
			};

			// Add Role To Claim If Exist

			var roles = await userManager.GetRolesAsync(user);

			foreach (var role in roles)
				//    authclaim.Add(new Claim("role", role));
				authclaim.Add(new Claim(ClaimTypes.Role, role));


			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtoptions.SecretKey));

			var signingcreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				audience: Jwtoptions.Audience,
				issuer: Jwtoptions.Issure,
				expires: DateTime.UtcNow.AddDays(Jwtoptions.DurationInDays),
				claims: authclaim,
				signingCredentials: signingcreds
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task SendResetPasswordEmail(ForgotPasswordRequest request)
		{
			var user = await userManager.FindByEmailAsync(request.Email);
			var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);

			if (user is not null)
			{
				//    var passwordUrl= Url
			}
		}

		public async Task<UserResultDTO> LoginWithGoogleAsync(GoogleLoginDto dto)
		{
			var response = await _httpClient.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={dto.IdToken}");

			if (!response.IsSuccessStatusCode)
				throw new Exception("Invalid Google token");

			var content = await response.Content.ReadAsStringAsync();

			var payload = JsonSerializer.Deserialize<GooglePayload>(content, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			if (payload == null || string.IsNullOrEmpty(payload.Email))
				throw new Exception("Failed to parse Google token payload");
			var userInDb = await userManager.FindByEmailAsync(payload.Email);

			// user = data
			// user = null => create

			if (userInDb == null)
			{
				// أول مرة يسجل دخول - اعمله Register تلقائي
				userInDb = new User
				{
					Email = payload.Email,
					DisplayName = payload.Name,
					UserName = dto.Username
				};

				var result = await userManager.CreateAsync(userInDb);
				if (!result.Succeeded)
				{
					var errors = result.Errors.Select(e => e.Description).ToList();
					throw new ValidationException(errors);
				}

				await userManager.AddToRoleAsync(userInDb, dto.Role);

				// سستم انتا الموضوع عشان انا رايح المحل دلوقتى عايزنى هناك ومش عارف هرجع امتا
				// دول الداتا اللى المفروض تاخدها عشان تعمل add new patient موجودين ف ال admin service 
				// لو عايز تعرف انا عامل ريجيستر للمريض ازاى ممكن تبص عليها 
				// AdminService >> AddPatientAsync

				// role -> patient
				//var patient = new Patient()
				//{
				//	UserName = _patient.UserName,
				//	Name = _patient.Name,
				//	Address = _patient.Address,
				//	BirthDate = _patient.BirthDate,
				//	Phone = _patient.PhoneNumber,
				//	Gender = _patient.Gender,
				//};
				//await unitOfWork.GetRepository<Patient, int>().AddAsync(patient);
				//await unitOfWork.SaveChangesAsync();



			}

			return new UserResultDTO(
				 payload.Name,
				 payload.Email,
				 await CreateTokenAsync(userInDb),
				 dto.Username);
		}
	}
}