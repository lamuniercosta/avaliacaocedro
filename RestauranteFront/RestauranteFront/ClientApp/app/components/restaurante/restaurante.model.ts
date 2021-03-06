﻿import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable, Subject } from 'rxjs/Rx';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class RestauranteService {
    constructor(private _http: Http) { };
    private headers = new Headers({ 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' });
    private RegenerateData = new Subject<number>();
    RegenerateData$ = this.RegenerateData.asObservable();
    action: string

    LoadData(): Promise<IRestaurante[]> {
        return this._http.get('http://localhost:50137/api/Restaurante')
            .toPromise()
            .then(response => this.extractArray(response))
            .catch(this.handleErrorPromise);
    }

    GetRestaurante(id: number): Promise<IRestaurante> {
        return this._http.get('http://localhost:50137/api/Restaurante/' + id, this.headers)
            .toPromise()
            .then(response => response.json() as IRestaurante)
            .catch(this.handleErrorPromise);
    }

    GetRestauranteByName(name: string): Observable<Response> {
        return this._http.get('http://localhost:50137/api/Restaurante/GetByName/' + name)
            .map(response => response)
            .catch(this.handleErrorPromise);
    }

    SaveRestaurante(id: number, model: RestauranteModel): Observable<Response> {
        this.action = id == 0 ? 'InsertRestaurante' : 'UpdateRestaurante';
        return this._http
            .post('http://localhost:50137/api/Restaurante/' + this.action, model, { headers: this.headers })
            .map(response => response)
            .catch(this.handleErrorPromise);
    }

    DeleteRestaurante(id: number): Observable<Response>{
        return this._http
            .post('http://localhost:50137/api/Restaurante/DeleteRestaurante', id, { headers: this.headers })
            .map(response => response)
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
    nome: string;
}

export class RestauranteModel {
    constructor(
        id: number,
        nome: string
    ) { }
}