namespace EcWebapi.Dto
{
    public class MemberDto : EntityDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateOnly? Birth { get; set; }

        /// <summary>
        /// 性別(0:男、1:女、2:不分)
        /// </summary>
        public int Gender { get; set; }
    }
}