using FinancasApp.Domain.Helpers;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;

        public UserService(IUserRepository userRepository, IProfileRepository profileRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }

        public void CreateAccount(User user)
        {
            var userExist = _userRepository.GetUserByEmail(user.Email);

            if (userExist != null)
                throw new ArgumentException("E-mail já cadastrado.");

            user.Password = Sha1Helper.ComputeSHA1Hash(user.Password);
            user.ProfileId = _profileRepository.GetProfileByName("cc").Id;

            _userRepository.Add(user);
        }

        public User Authenticate(string email, string password)
        {
            password = Sha1Helper.ComputeSHA1Hash(password);

            var user = _userRepository.GetUserByEmailAndPassword(email, password);

            if (user == null)
            {
                throw new ArgumentException("Login ou senha incorretos.");

            }

            return user;
        }
    }
}
