import React, { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import axios from "axios";
import { EditingState } from '@devexpress/dx-react-grid';
import CircularProgress from '@mui/material/CircularProgress';
import { useNavigate } from "react-router-dom";
import {
    PagingState,
    IntegratedPaging,
} from '@devexpress/dx-react-grid';
import {
    Grid,
    Table,
    TableHeaderRow,
    TableEditRow,
    TableEditColumn,
    PagingPanel,
} from '@devexpress/dx-react-grid-material-ui';
import { SuccessSnackBar } from './SuccessSnackBar';

const editColumnMessages = {
    addCommand: 'Додати',
    editCommand: 'Редагувати',
    deleteCommand: 'Видалити',
    commitCommand: 'Зберегти',
    cancelCommand: 'Відмінити',
};

const columns = [
    { name: 'name', title: 'Назва' }
];

const getRowId = row => row.id;

export function SchedulesList(props) {
    const [rows, setRows] = useState([]);
    const [loading, setLoading] = useState(true);
    const [snackbarState, setSnackbarState] = useState({
        open: false,
        message: ''
    });

    const navigate = useNavigate();
    useEffect(() => {
        axios.get('api/schedules').then((response) => {
            setRows(response.data);
            setLoading(false)
        })
    }, [])

    const handleSnackbarClose = (event, reason) => {
        setSnackbarState({
            ...snackbarState,
            open: false
        });
    };

    const TableRow = ({ row, ...restProps }) => (
        <Table.Row
            {...restProps}
            onClick={() =>navigate(`/schedule/${row.id}`)}
            style={{
                cursor: 'pointer'
            }}
        />
    );

    const commitChanges = ({ added, changed, deleted }) => {
        let changedRows;
        if (deleted) {
            axios.delete(`api/schedules/${deleted[0]}`).then((response) => {
                setSnackbarState({ open: true, message: 'Вхідні дані успішно видалені!' })
                const deletedSet = new Set(deleted);
                changedRows = rows.filter(row => !deletedSet.has(row.id));
                setRows(changedRows);
            })
        }
    };



    let contents = loading
        ? <Box sx={{ display: 'flex' }}>
            <CircularProgress />
        </Box>
        : <Box sx={{ height: 400, width: '100%', mt: 4 }}>
            <Grid
                rows={rows}
                columns={columns}
                getRowId={getRowId}
            >
                <PagingState
                    defaultCurrentPage={0}
                    pageSize={5}
                />
                
                <IntegratedPaging />
                <EditingState
                    onCommitChanges={commitChanges}
                />
                <Table rowComponent={TableRow} />
                <TableHeaderRow />
                <TableEditRow />
                <TableEditColumn
                    showDeleteCommand
                    messages={editColumnMessages}
                />
                <PagingPanel />
            </Grid>
        </Box>;

    return (
        <div>
            {contents}
        </div>
    );
}
