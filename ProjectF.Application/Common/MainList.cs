using System.Collections.Generic;
using ProjectF.Data.Entities.RequestFeatures;

namespace ProjectF.Application.Common
{
    public interface IMainList<T> 
    {
        public IList<T> List { get ; }
        public MetaData Meta { get; }
    }
}
