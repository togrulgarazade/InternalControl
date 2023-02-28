using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.ViewModels.Account
{
    public class LogRegViewModel
    {

        #region LoginVM


        public string Email { get; set; }
        public bool RememberMe { get; set; }

        #endregion


        #region RegisterVM


        [DataType(DataType.Password)]
        public string Password { get; set; }



        #endregion

    }
}
