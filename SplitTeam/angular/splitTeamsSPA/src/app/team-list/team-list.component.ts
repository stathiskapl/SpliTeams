import { Component, OnInit } from '@angular/core';
import { TeamService } from '../Services/team.service';
import { Team } from '../_modules/team';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.css']
})
export class TeamListComponent implements OnInit {
  role: string;
  constructor(private teamService: TeamService) { }
  teamName = '';

  teams: Team[];
  ngOnInit() {
    this.role = localStorage.getItem('role').toString();
    this.getAllTeams();
  }
  addTeam() {
    const teamToadd: Team = {};
    teamToadd.name = this.teamName;
    this.teamService.createTeam(teamToadd).subscribe((data: Team) => {
      this.getAllTeams();
      this.teamName = '';
    },
      error => {
      });
  }

  getAllTeams() {
    this.teamService.getAllTeams().subscribe((data: Team[]) => {
      this.teams = data;
    }, error => {
    });
  }

}
