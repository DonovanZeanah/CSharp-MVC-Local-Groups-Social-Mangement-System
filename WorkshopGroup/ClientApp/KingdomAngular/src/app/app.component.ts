import { Component } from '@angular/core';
import { Skill } from './models/skill';
import { SkillService } from './services/skill.service';

/*
swagger that can also create a swagger.json file and that is the input for the npm package ng-openapi-gen,
 to generate all the api services and models in Angular. Sync the WebApi changes to your Angular
 code can be done very easy and fast.
 And also use Angular Material, that makes your website for users much better and your life as
  a programmer much easier.
*/
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'KingdomAngular';
  skills : Skill[] = [];

  constructor(private skillService: SkillService) { } //this.skills = this.skillService.getSkills();}

  ngOnInit() : void {
    this.skills = this.skillService.getSkills();
    console.log(this.skills);
  }
}
