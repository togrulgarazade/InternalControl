using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.ViewModels.Account
{
    public class UserSettingsViewModel
    {
        [Required, MaxLength(255), DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }



        public string Phone { get; set; }
        public string Fullname { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }




    }
}
