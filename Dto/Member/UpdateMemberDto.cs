﻿using System.ComponentModel.DataAnnotations;

namespace EcWebapi.Dto.Member
{
    public class UpdateMemberDto : EntityDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Required]
        public DateOnly Birth { get; set; }

        /// <summary>
        /// 性別(0:男、1:女、2:不分)
        /// </summary>
        [Required]
        public int Gender { get; set; }
    }
}