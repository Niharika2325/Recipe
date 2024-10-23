using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using CookingRecipe.Models;


namespace CookingRecipe.DAL.Interfaces
{
    public interface IProfile
    {
        IEnumerable<User> GetUserById(int id);
        void UpdateUserField(int id, UserFieldUpdate model);
    }
}