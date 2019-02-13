import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import { Route, Switch } from 'react-router'
import { withRouter } from 'react-router'
import Shop from './Shop'
import OrderProducts from './Products'
import Details from './Details'

import { Container } from './styled'

class NewOrder extends Component {

    state = {
        basket: null,
        shopId: ''
    }

    updateBasket = (value) => {
        this.setState({ basket: value })
    }

    selectShop = (id) => {
        this.setState({ shopId: id })
    }

    render() {

        const { order, shopId } = this.state

        return (
            <Container>
                <span>Новый заказ</span>
                <div>
                    <Link to="/">На главную</Link>
                </div>
                <div>
                    <Switch>
                        <Route exact path="/orders/create" render={(props) => (<Shop {...props} onSelect={this.selectShop} />)} />
                        <Route path="/orders/create/products" render={(props) => (<OrderProducts {...props} updateBasket={this.updateBasket} shop={shopId} />)} />
                        <Route path="/orders/create/details" render={(props) => (<Details {...props} order={this.state} />)} />
                    </Switch>
                </div>
            </Container>
        )
    }
}

export default withRouter(NewOrder);