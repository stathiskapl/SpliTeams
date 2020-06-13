import { Component, OnInit } from '@angular/core';
import { MatchService } from '../Services/match.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Match } from '../_modules/match';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.css']
})
export class MatchListComponent implements OnInit {
  role: string;
  matches: Match[];
  constructor(private matchService: MatchService, private router: Router, private toastr: ToastrService) {
    this.role = localStorage.getItem('role');
  }
  ngOnInit() {
    this.role = localStorage.getItem('role').toString();
    this.getAllMatches();
  }
  getAllMatches() {
    this.matchService.getAllMatches().subscribe((data: Match[]) => {
      this.matches = data;
    }, error => {
      console.log(error);
      this.toastr.error(error.console.error);
    });
  }
}
