﻿using BL.Managers.Interfaces;
using Data.Database;
using Data.Respositories;
using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class UserManagerNoDI : IUserManager
    {
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
            LMSEntities context = new LMSEntities();
            UserRepository userRepository = new UserRepository(context);
            createdUser = userRepository.Add(createdUser);

            UserDisplayDto displayUser = new UserDisplayDto
            {
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
            LMSEntities context = new LMSEntities();
            UserRepository userRepository = new UserRepository(context);
            return userRepository.FindUser(userName, passwordHash);
        }

    }

}
