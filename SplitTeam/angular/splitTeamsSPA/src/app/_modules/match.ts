import { Team } from './team';

export interface Match {
    id?: number;
    teamA?: Team;
    teamB?: Team;
    scoreTeamA?: number;
    scoreTeamB?: number;
    description?: string;
}
