import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { UserRating } from '../../UserRating/userrating.component';

@Component({
  selector: 'my-card',
  imports: [MatCardModule, UserRating],
  templateUrl: './small.component.html',
  standalone: true,
})
export class SmallCard { }
