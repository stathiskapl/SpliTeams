import { Component, OnInit } from '@angular/core';
import { SkillService } from '../Services/skill.service';
import { Skill } from '../_modules/skill';

@Component({
  selector: 'app-skill-list',
  templateUrl: './skill-list.component.html',
  styleUrls: ['./skill-list.component.css']
})
export class SkillListComponent implements OnInit {

  constructor(private skillService: SkillService) { }
  skillName = '';

  skills: Skill[];
  ngOnInit() {
    this.getAllSkills();
  }
  addSkill() {
    const skillToadd: Skill = {};
    skillToadd.name = this.skillName;
    this.skillService.createSkill(skillToadd).subscribe((data: Skill) => {
      this.getAllSkills();
      this.skillName = '';
    },
      error => {
        console.log(error.message);
      });
  }

  getAllSkills() {
    this.skillService.getAllSkills().subscribe((data: Skill[]) => {
      this.skills = data;
      console.log(this.skills);
    }, error => {
      console.log(error.message);
    });
  }
}
