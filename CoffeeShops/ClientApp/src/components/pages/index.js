import React, { Component } from 'react'
import { Route, Switch } from 'react-router'
import NewClient from './Client/NewClient'
import NewShop from './Shop/NewShop'
import Client from './Client'
import Shop from './Shop'
import Home from './Home'
import Products from './Shop/Products'
import NewProduct from './Shop/Products/NewProduct'

import { Container } from './styled'


class Pages extends Component {

    render() {
        return (
            <Container>
                <Switch>
                    <Route exact path="/" component={Home} />
                    <Route exact path="/clients" component={Client} />
                    <Route exact path="/clients/create" component={NewClient} />
                    <Route path="/clients/edit/:id" component={NewClient} />
                    <Route exact path="/shops" component={Shop} />
                    <Route exact path="/shops/create" component={NewShop} />
                    <Route path="/shops/edit/:id" component={NewShop} />
                    <Route path="/shops/products/list/:shopId" component={Products} />
                    <Route path="/shops/products/create/:shopId" component={NewProduct} />
                    <Route path="/shops/products/edit/:id" component={NewProduct} />
                </Switch>
            </Container>
        );
    }
}

export default Pages;