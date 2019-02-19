import React, { Component } from 'react'
import { Route, Switch } from 'react-router'
import NewClient from './Client/NewClient'
import NewShop from './Shop/NewShop'
import Client from './Client'
import Shop from './Shop'
import Home from './Home'
import Products from './Shop/Products'
import NewProduct from './Shop/Products/NewProduct'
import NewOrder from './Order/NewOrder'
import Order from './Order'
import Login from './Login'

import { Container } from './styled'
import { PrivateRoute } from '../layouts/PrivateRoute';


class Pages extends Component {

    render() {
        return (
            <Container>
                <Switch>
                    <Route path="/login" component={Login} />
                    <PrivateRoute exact path="/" component={Home} />
                    <PrivateRoute exact path="/clients" component={Client} />
                    <PrivateRoute exact path="/clients/create" component={NewClient} />
                    <PrivateRoute path="/clients/edit/:id" component={NewClient} />

                    <PrivateRoute exact path="/shops" component={Shop} />
                    <PrivateRoute exact path="/shops/create" component={NewShop} />
                    <PrivateRoute path="/shops/edit/:id" component={NewShop} />
                    <PrivateRoute path="/shops/products/list/:shopId" component={Products} />
                    <PrivateRoute path="/shops/products/create/:shopId" component={NewProduct} />
                    <PrivateRoute path="/shops/products/edit/:id" component={NewProduct} />

                    <PrivateRoute path="/orders/create" component={NewOrder} />
                    <PrivateRoute path="/orders" component={Order} />
                </Switch>
            </Container>
        );
    }
}

export default Pages;