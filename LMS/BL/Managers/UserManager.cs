﻿using BL.Managers.Interfaces;
using Data.Respositories.Interfaces;
using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class UserManager : IUserManager
    {
        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDisplayDto CreateUser(UserRegisterDto user)
        {
            User createdUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = Utils.HashHelper.GetMD5HashData(user.Password),
                UserName = user.UserName,
                CreatedOn = DateTime.Now
            };
            createdUser = _userRepository.Add(createdUser);

            UserDisplayDto displayUser = new UserDisplayDto
            {
                Id = createdUser != null ? createdUser.Id : 0,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return displayUser;
        }

        public User FindUser(string userName, string password)
        {
            var passwordHash = Utils.HashHelper.GetMD5HashData(password);
            return _userRepository.FindUser(userName, passwordHash);
        }
    }

}
