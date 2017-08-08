import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { RestauranteService, IRestaurante, RestauranteModel } from './restaurante.model';
import { Location } from '@angular/common';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

@Component({
    selector: "editRestaurante",
    templateUrl: 'edit.restaurante.component.html',
    providers: [RestauranteService]
})

export class EditRestaurante implements OnInit {
    model: RestauranteModel;
    id: number;
    constructor(private service: RestauranteService, private route: ActivatedRoute, private location: Location, private router: Router)
    { }


    ngOnInit(): void {
        this.id = this.route.snapshot.params['id'];
        this.service.GetRestaurante(this.id).then(model => this.model = model);
    }

    SaveRestaurante() {
        this.service.SaveRestaurante(this.model).then(() => this.goBack());
    }

    goBack(): void {
        this.router.navigate(['/restaurante']);
    }
}