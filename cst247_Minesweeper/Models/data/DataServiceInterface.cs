using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cst247_Minesweeper.Models.data
{
    interface DataServiceInterface<T>
    {
        bool Create(T newT);
        T Read(int id);
        List<T> ReadAll();
        bool Update(T updatedT);
        bool Delete(int id);
    }
}