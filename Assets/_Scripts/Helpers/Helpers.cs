public static class Helpers
{
   public static Enums.Directions Compress8to4Directions(Enums.Directions direction)
   {
      //convert 8-way to 4-way direction for animation purposes
      if ((int)direction >= 4)
      {
         direction -= 4;
      }

      return direction;
   }
}
