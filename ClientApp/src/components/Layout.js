import React, { Component } from 'react';
import { Container } from 'reactstrap';
import ResponsiveAppBar from './ResponsiveAppBar';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
        <div>
            <ResponsiveAppBar />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
