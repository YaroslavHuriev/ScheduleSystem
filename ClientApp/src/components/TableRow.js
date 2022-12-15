import { Table } from '@devexpress/dx-react-grid-material-ui';
import React, { useState } from 'react';

export function TableRow(props) {
    const [hover, setHover] = useState(false);

    const { onClick, style, onMouseEnter, onMouseLeave, ...restProps } = props
    const toggleHover = () => {
        setHover(!hover)
    }
    let linkStyle;
    if (hover) {
        linkStyle = { backgroundColor: 'rgba(0, 0, 0, 0.04)', cursor: 'pointer' }
    } else {
        linkStyle = { color: '#000' }
    }
    return (
        <Table.Row
            {...restProps}
            onClick={props.onClick}
            style={linkStyle}
            onMouseEnter={toggleHover}
            onMouseLeave={toggleHover}
        />
    )
}