﻿namespace Shared.PatientModels
{
	public record AvailableTimesDto
	{
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public bool IsAvailable { get; set; }
	}
}
