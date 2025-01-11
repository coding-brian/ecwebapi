namespace EcWebapi.Dto
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        /// <summary>
        /// 幾秒後到期
        /// </summary>
        public double ExpireIn { get; set; }
    }
}