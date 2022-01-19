using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perstistance
{
    public interface IFakeBeerRepository
    {
        BeerEntity Create(BeerEntity beerEntityToCreate);

        //IList<BeerEntity> GetAll();
        IEnumerable<BeerEntity> GetAll();

        BeerEntity? GetById(Guid id);

        BeerEntity? GetByName(string name);

        bool DeleteById(Guid id);

    }
}
