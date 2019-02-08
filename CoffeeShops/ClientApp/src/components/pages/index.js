import React, { Component } from 'react'
import { Route, Switch } from 'react-router'
import NewClient from './Client/NewClient'
import NewShop from './Shop/NewShop'
import Client from './Client'
import Shop from './Shop'
import Home from './Home'

import { Container } from './styled'


class Pages extends Component {

    render() {
        return (
            <Container>
                <Switch>
                    <Route exact path="/" component={Home} />
                    <Route exact path="/create/client" component={NewClient} />
                    <Route exact path="/create/shop" component={NewShop} />
                    <Route exact path="/clients" component={Client} />
                    <Route exact path="/shops" component={Shop} />
                </Switch>
            </Container>
        );
    }
}

export default Pages;