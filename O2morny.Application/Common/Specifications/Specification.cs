using LinqKit;
using System.Linq.Expressions;

namespace O2morny.Application.Common.Specifications
{
    public abstract class Specification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; } = x => true;

        public Specification<T> And(Expression<Func<T, bool>> expression)
        {
            Criteria = Criteria.And(expression);
            return this;
        }
    }
}
