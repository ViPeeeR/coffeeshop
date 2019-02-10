import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import { Route, Switch } from 'react-router'
import { withRouter } from 'react-router'
import Shop from './Shop'
import OrderProducts from './Products'

import { Container } from './styled'

class NewOrder extends Component {

    render() {
        return (
            <Container>
                <span>Новый заказ</span>
                <div>
                    <Link to="/">На главную</Link>
                </div>
                <div>
                    <Switch>
                        <Route exact path="/orders/create" component={Shop} />
                        <Route path="/orders/create/products/:shopId" component={OrderProducts} />
                    </Switch>
                </div>
            </Container>
        )
    }
}

export default withRouter(NewOrder);