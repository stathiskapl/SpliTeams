import { Component, OnInit } from '@angular/core';
import { StatsService } from '../_services/stats.service';
import { PlayerStatsDto } from '../_modules/playerStatsDto';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {
  playerStats: PlayerStatsDto[];

  constructor(private statService: StatsService) { }
  ngOnInit() {
    this.getMatchesWithTeamsAndPlayers();
  }

  getMatchesWithTeamsAndPlayers() {
    this.statService.getMatchesWithTeamsAndPlayers().subscribe((data: PlayerStatsDto[]) => {
      this.playerStats = data;
    }, error => {
    });
  }
}
