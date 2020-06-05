import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../Services/player.service';
import { Player } from '../_modules/player';
import { ToastrService } from 'ngx-toastr';
import { NgbModalOptions, ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../Services/user.service';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {
  role: string;
  closeResult: string;
  modalOptions: NgbModalOptions;
  constructor(private playerService: PlayerService, private toastr: ToastrService, private modalService: NgbModal, private userService: UserService) {
    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'
    };
    this.role = localStorage.getItem('role').toString();
    //this.role = userService.decodedToken.role;
  }
  playerName = '';

  players: Player[];
  ngOnInit() {
    this.role = localStorage.getItem('role').toString();
    //this.role = this.userService.decodedToken.role;
    this.getAllPayers();
  }

  open(content, playerId) {
    this.modalService.open(content, this.modalOptions).result.then((result) => {
      if (result === 'Delete') {
        console.log('diagrafi playerId');
        console.log(playerId);
        this.deletePlayerWithRanks(playerId);
      } else if (result === 'Exit') {
        console.log('akuro');
      }
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      console.log('by pressing ESC');
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      console.log('by clicking on a backdrop');
      return 'by clicking on a backdrop';
    } else if (reason === 'Cross click') {
      console.log('Cross click');
      return `with: ${reason}`;
    } else {
      console.log(`with: ${reason}`);
      return `with: ${reason}`;
    }
  }
  deletePlayer(playerId: number) {

    this.playerService.deletePlayer(playerId).subscribe(() => {
      this.getAllPayers();
      this.toastr.success('Player Deleted');
    },
      error => {
        console.log(error.message);
        this.toastr.error('Error!', error.message);
      });
  }
  addPlayer() {
    const playerToadd: Player = {};
    playerToadd.name = this.playerName;
    this.playerService.createPlayer(playerToadd).subscribe((data: Player) => {
      this.getAllPayers();
      this.playerName = '';
    },
      error => {
        console.log(error.message);
      });
  }

  getAllPayers() {
    this.playerService.getAllPayers().subscribe((data: Player[]) => {
      this.players = data;
      console.log(this.players);
    }, error => {
      console.log(error.message);
    });
  }

  deletePlayerWithRanks(playerId: number) {

    this.playerService.deletePlayerWithRanks(playerId).subscribe(() => {
      this.getAllPayers();
      this.toastr.success('Player Deleted');
    },
      error => {
        console.log(error.message);
        this.toastr.error('Error!', error.message);
      });
  }
}
