import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { RestauranteService, IRestaurante, RestauranteModel } from './restaurante.model';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ConfirmationDialogComponent } from '../confirm/confirmation-dialog.component';
   

@Component({
    selector: "restaurante",
    templateUrl: 'restaurante.component.html'
})

export class Restaurante {

    model = new RestauranteModel(0, '');
    restaurante: IRestaurante;
    message: string;

    constructor(private service: RestauranteService, private router: Router) {

    }
    public restaurantes: IRestaurante[];

    GetRestaurantes(name: string) {
        this.restaurantes = [];
        this.service.GetRestauranteByName(name).subscribe((response) => {
            if (response.status == 200) {
                this.restaurante = response.json() as IRestaurante;
                this.restaurantes.push(this.restaurante);
            }
            else {
                this.message = '<div class="alert alert-danger" role="alert">' + response.text() + '</div>';
            }
        });
    }

    EditRestaurante(id: number) {
        this.router.navigate(['/editrestaurante/', id]);
    }

    DeleteRestaurante(id: number) {
        this.router.navigate(['/confirmation/', id, 'restaurante']);
    }
}
