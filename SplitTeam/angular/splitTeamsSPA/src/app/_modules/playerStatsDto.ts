import { Player } from './player';

export interface PlayerStatsDto {
    player: Player;
    wins?: number;
    losses?: number;
    draws?: number;
}