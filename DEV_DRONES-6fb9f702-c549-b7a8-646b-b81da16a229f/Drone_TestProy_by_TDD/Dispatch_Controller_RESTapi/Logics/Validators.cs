using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_Controller_RESTapi.Logics
{
    public static class Validators
    {

        internal static bool CheckMedicationCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            
            char[] invalidCharacters = "abcdefghijklmnopqrstuvwxyz`~-!@#$%^&*()+=<>,.?/\\|{}[]'\"".ToCharArray();
             
            return code.IndexOfAny(invalidCharacters) >= 0 ? false : true;
        }

        internal static bool CheckMedicationName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            
               char[] invalidCharacters = "`~!@#$%^&*()+=<>,.?/\\|{}[]'\"".ToCharArray();

            return name.IndexOfAny(invalidCharacters) >= 0 ? false : true;
        }
    }
}