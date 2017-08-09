import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable, Subject } from 'rxjs/Rx';
import { IRestaurante } from '../restaurante/restaurante.model';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class PratoService {
    constructor(private _http: Http) { };
    private headers = new Headers({ 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' });
    private RegenerateData = new Subject<number>();
    RegenerateData$ = this.RegenerateData.asObservable();
    action: string

    LoadData(): Promise<IPrato[]> {
        return this._http.get('http://localhost:50137/api/Prato')
            .toPromise()
            .then(response => this.extractArray(response))
            .catch(this.handleErrorPromise);
    }

    GetRestaurantes(): Promise<IRestaurante[]> {
        return this._http.get('http://localhost:50137/api/Restaurante')
            .toPromise()
            .then(response => this.extractArray(response))
            .catch(this.handleErrorPromise);
    }

    GetPrato(id: number): Promise<IPrato> {
        return this._http.get('http://localhost:50137/api/Prato/' + id, this.headers)
            .toPromise()
            .then(response => response.json() as IPrato)
            .catch(this.handleErrorPromise);
    }

    GetPratoByName(name: string): Observable<Response> {
        return this._http.get('http://localhost:50137/api/Prato/GetByName/' + name)
            .map(response => response)
            .catch(this.handleErrorPromise);
    }

    SavePrato(id: number, model: PratoModel): Observable<Response> {
        this.action = id == 0 ? 'InsertPrato' : 'UpdatePrato';
        return this._http
            .post('http://localhost:50137/api/Prato/' + this.action, model, { headers: this.headers })
            .map(response => response)
            .catch(this.handleErrorPromise);
    }

    DeletePrato(id: number): Observable<Response>{
        return this._http
            .post('http://localhost:50137/api/Prato/DeletePrato', id, { headers: this.headers })
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

export interface IPrato {
    id: number;
    restauranteId: number;
    nome: string;
    preço: number;
}

export class PratoModel {
    constructor(
        id: number,
        restauranteId: number,
        nome: string,
        preço: number
    ) { }
}