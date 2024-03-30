import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Item} from "./item.model";


@Injectable({
  providedIn: 'root'
})
export class ItemService {
  url = environment.apiBaseUrl + "/Item";
  itemsList: Item[] = [];
  constructor(private http: HttpClient) {
  }

  getItemList() {
    this.http.get(this.url).subscribe({
      next: res => {
        this.itemsList = res as Item[];
      },
      error: err => {
        console.log(err);
      }
    });
  }
}
