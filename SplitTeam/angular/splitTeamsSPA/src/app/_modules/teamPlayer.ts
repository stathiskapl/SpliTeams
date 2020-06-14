import { Player } from './player';
import { Team } from './team';
export interface TeamPlayer {
    id: number;
    player: Player;
    team: Team;
}