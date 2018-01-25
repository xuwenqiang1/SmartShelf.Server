using SmartShelf.DataAccess;
using SmartShelf.Model;

namespace SmartShelf.Logic
{
    public class UserLogic
    {
        private readonly UserDataAccess _userDataAccess;
        public UserLogic()
        {
            _userDataAccess = new UserDataAccess();
        }

        public void Add(User user)
        {
            //Validation and logic
            _userDataAccess.Add(user);
        }
    }
}
