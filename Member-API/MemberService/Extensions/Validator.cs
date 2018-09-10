namespace MemberService.Extensions
{
    public static class Validator
    {
        public static bool ValidateId<T>(this T entity, long id)
        {
            return entity.GetType().GetProperty("Id").GetValue(entity).Equals(id);
        }
    }
}
