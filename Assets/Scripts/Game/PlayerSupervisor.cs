using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupervisor : Singleton<PlayerSupervisor>
{
    public List<Player> players;
	
    public Player GetFirstPlayer()
    {
        if (players.Count > 0)
            return players[0];
        else
            return null;
    }

}
