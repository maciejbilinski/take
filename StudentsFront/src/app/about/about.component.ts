import { Component } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrl: './about.component.css'
})
export class AboutComponent {
  now!: Date;
  private subscription: any;


  ngOnInit(){
    this.setDate();
    this.subscription = setInterval(() => this.setDate(), 1000);
  }

  ngOnDestroy(){
    if(this.subscription){
      clearInterval(this.subscription);
    }
  }

  setDate(){
    this.now = new Date();
  }
}
