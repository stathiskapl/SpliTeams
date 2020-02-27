import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../Services/player.service';
import { Player } from '../_modules/player';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {

  constructor(private playerService: PlayerService) { }
  players: Player[];
  ngOnInit() {
    this.getAllPayers();
  }

  getAllPayers() {
    this.playerService.getAllPayers().subscribe((data: Player[]) => {
      this.players = data;
      console.log(this.players);
    }, error => {
      console.log(error.message);
    });
  }
}
