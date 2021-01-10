using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications
{
    public class OperationFilterPaginatedSpecification : Specification<Operation>
    {
        public OperationFilterPaginatedSpecification(int skip, int take)
            : base()
        {
            Query.Include(x => x.User);
            Query.Skip(skip).Take(take);
        }
    }
}
