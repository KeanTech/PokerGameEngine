using GameEngine.Models.Game;

namespace GameEngine.Core.Extentions
{
    public static class ModelExtention
    {
        public static User CreateDaoModel(this GameUser gameUser, Player player)
        {
            User user = new User();
            user.Id = gameUser.Id;
            user.Name = gameUser.Name;
            user.UserIdentifier = gameUser.UserIdentifier;
            user.Wins = gameUser.Wins;
            user.Accessories = gameUser.Accessories;
            user.ChipsAquired = gameUser.ChipsAquired;
            user.Player = player;

            return user;    
        }
        public static Player CreateDaoModel(this Player player, User user)
        {
            
        }


        public static GameUser CreateGameModel(this User user) 
        {
            GameUser gameUser = new GameUser();
            gameUser.Id = user.Id;
            gameUser.Name = user.Name;
            gameUser.UserIdentifier = user.UserIdentifier;
            gameUser.Wins = user.Wins;
            gameUser.Accessories = user.Accessories;
            gameUser.ChipsAquired = user.ChipsAquired;

            return gameUser;
        }
    
            
    
    
    }
}
