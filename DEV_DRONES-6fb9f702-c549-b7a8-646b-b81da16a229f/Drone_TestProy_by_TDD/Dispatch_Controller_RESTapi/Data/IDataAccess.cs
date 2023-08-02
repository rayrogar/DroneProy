using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Dispatch_Controller_RESTapi.Data
{
    public interface IDataAccess
    {
        List<T> LoadData<T>(){
            throw new NotImplementedException();
        }

         object SaveData<T>(T data){
            throw new NotImplementedException();
        }

        object UpdateData<T>(T data)
        {
            throw new NotImplementedException();
        }
    }
}