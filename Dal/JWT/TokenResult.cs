namespace Dal.JWT
{
    public class TokenResult
    {
        /// <summary>
        /// token字符串
        /// </summary>
        public string Access_token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expires_in { get; set; }
    }
}
