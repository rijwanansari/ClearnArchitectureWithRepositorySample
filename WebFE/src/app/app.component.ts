import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public appSettings?: AppSetting[];

  constructor(http: HttpClient) {
    http.get(BASE_URL + '/api/MasterAPI/GetAppSettingsAsync').subscribe((result:any) => {
      this.appSettings = result.output;
    }, error => console.error(error));
  }

  title = 'My App';
}

interface AppSetting {
  id: number;
  referenceKey: string;
  value: string;
  description: number;
  type: string;
}
const BASE_URL = "https://localhost:7177";
