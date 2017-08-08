import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { RestauranteService, IRestaurante, RestauranteModel } from './restaurante.model';


@Component({
    selector: "restaurante",
    templateUrl: 'restaurante.component.html'
})

export class Restaurante {

    model = new RestauranteModel('', 0, 0, '');
    constructor(private service: RestauranteService) {

    }
    public restaurantes: IRestaurante[];

    GetRestaurantes() {        
        this.service.LoadData().then(data => {
            this.restaurantes = data;
        })
    }
}

    
