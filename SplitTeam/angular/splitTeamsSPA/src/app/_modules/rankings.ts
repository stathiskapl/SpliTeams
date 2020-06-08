import { Player } from './player';
import { Skill } from './skill';

export interface Ranking {
    id: number;
    player: Player;
    skill: Skill;
    rank: number;
    userId: number;
}
