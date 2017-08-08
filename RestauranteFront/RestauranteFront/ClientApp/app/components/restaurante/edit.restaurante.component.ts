import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { RestauranteService, IRestaurante, RestauranteModel } from './restaurante.model';
import { Location } from '@angular/common';
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
    selector: "editRestaurante",
    templateUrl: 'edit.restaurante.component.html',
    providers: [RestauranteService]
})

export class EditRestaurante implements OnInit {
    model: RestauranteModel;
    constructor(private service: RestauranteService, private route: ActivatedRoute, private location: Location)
    { }


    ngOnInit(): void {
        this.route.paramMap.switchMap((params: ParamMap) => this.service.GetRestaurante(+params.get('id'))).subscribe(model => this.model = model);
    }

    SaveRestaurante() {
        
    }

    goBack(): void {
        this.location.back();
    }
}