import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Customer} from "./customer.model";

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  url = environment.apiBaseUrl + "/Customer";
  customersList: Customer[] = [];

  constructor(private http: HttpClient) {
  }

  getClientsList() {
    this.http.get(this.url).subscribe({
      next: res => {
        this.customersList = res as Customer[];
      },
      error: err => {
        console.log(err);
      }
    });
  }
}
