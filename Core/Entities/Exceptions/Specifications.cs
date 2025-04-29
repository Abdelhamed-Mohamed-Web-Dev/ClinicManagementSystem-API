using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public abstract class Specifications<T> where T : class
    {

        protected Specifications(Expression<Func<T, bool>>? creiteria)
        {
            Creiteria = creiteria;
        }
        public Expression<Func<T, bool>>? Creiteria { get; }
        public List<Expression<Func<T, object>>> IncludeExperessions { get; } = new();
            
        protected void Includes(Expression<Func<T, object>> expression) => IncludeExperessions.Add(expression);
    
    }
}
