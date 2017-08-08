import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable, Subject } from 'rxjs/Rx';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class RestauranteService {
    constructor(private _http: Http) { };
    private headers = new Headers({ 'Content-Type': 'application/json' });
    private RegenerateData = new Subject<number>();
    RegenerateData$ = this.RegenerateData.asObservable();

    LoadData(): Promise<IRestaurante[]> {
        return this._http.get('/api/SampleData/WeatherForecasts')
            .toPromise()
            .then(response => this.extractArray(response))
            .catch(this.handleErrorPromise);
    }

    GetRestaurante(id: number): Promise<IRestaurante> {        
        return this._http.get('/api/SampleData/GetWeatherForecast/' + id)
            .toPromise()
            .then(response => response.json() as IRestaurante)
            .catch(this.handleErrorPromise);
    }

    SaveRestaurante(model: RestauranteModel) {
        return this._http
            .post('/api/SampleData/Add', JSON.stringify({ model: model }), { headers: this.headers })
            .toPromise()
            .then(res => res.json().data)
            .catch(this.handleErrorPromise);
    }

    protected extractArray(res: Response, showprogress: boolean = true) {
        let data = res.json();

        return data || [];
    }

    protected handleErrorPromise(error: any): Promise<void> {
        try {
            error = JSON.parse(error._body);
        } catch (e) {
        }

        let errMsg = error.errorMessage
            ? error.errorMessage
            : error.message
                ? error.message
                : error._body
                    ? error._body
                    : error.status
                        ? `${error.status} - ${error.statusText}`
                        : 'unknown server error';

        console.error(errMsg);
        return Promise.reject(errMsg);
    }

}

export interface IRestaurante {
    id: number;
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

export class RestauranteModel {
    constructor(
        id: number,
        dateFormatted: string,
        temperatureC: number,
        temperatureF: number,
        summary: string
    ) { }
}