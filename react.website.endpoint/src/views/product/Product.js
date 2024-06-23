import React from 'react'
import { CTable, CTableHead, CTableRow, CTableHeaderCell, CTableDataCell, CTableBody, CButton, CSpinner } from '@coreui/react';
import axios from "axios";

const Product = () => {

    const [product, setProduct] = React.useState([]);
    const result = [{
        page: 1,
        pageSize: 20,
        ProductTypeId: null,
        BrandId: null,
        AvailableStock: true,
        SearchKey: null,
        SortType: "none"
    }];
  React.useEffect(() => {
    axios({
            mode: 'no-cors',
            headers: { 'Content-Type': 'application/json'},
             url: 'http://localhost:5168/api/v1/Product/GetProducts',
             requestDto: result
          })
        .then(function (response) {
            setProduct(response.data.data);
        })
        .catch(function (error) {
            console.log(error);
        });
});
return (
    <>
      {product.length > 0 ? (
        <div style={{overflowX: 'auto'}}>
          
      <CTable>
      <CTableHead>
        <CTableRow>
          <CTableHeaderCell scope="col">#</CTableHeaderCell>
          <CTableHeaderCell scope="col">Image</CTableHeaderCell>
          <CTableHeaderCell scope="col">Name</CTableHeaderCell>
          <CTableHeaderCell scope="col">Slug</CTableHeaderCell>
          <CTableHeaderCell scope="col">Price</CTableHeaderCell>
          <CTableHeaderCell scope="col">AvailableStock</CTableHeaderCell>
          <CTableHeaderCell scope="col">RestockThreshold</CTableHeaderCell>
          <CTableHeaderCell scope="col">MaxStockThreshold</CTableHeaderCell>
          <CTableHeaderCell scope="col">Description</CTableHeaderCell>
        </CTableRow>
      </CTableHead>
      <CTableBody>
        {product.map((object, i) => (
          <CTableRow key={object.id}>
            <CTableHeaderCell scope="row">{object.id}</CTableHeaderCell>
            <CTableDataCell>
            <div className="avatar avatar-md"><a href={object.images} target='_blank'><img src={object.images} className="avatar-img" /></a><span className="avatar-status bg-success"></span></div>
            </CTableDataCell>
            <CTableDataCell>{object.name}</CTableDataCell>
            <CTableDataCell>{object.slug}</CTableDataCell>
            <CTableDataCell>{object.price}</CTableDataCell>
            <CTableDataCell>{object.availableStock}</CTableDataCell>
            <CTableDataCell>{object.restockThreshold}</CTableDataCell>
            <CTableDataCell>{object.maxStockThreshold}</CTableDataCell>
            <CTableDataCell>{object.description}</CTableDataCell>
          </CTableRow>
        ))}
      </CTableBody>
    </CTable>
        </div>
    ) : (
<div className="text-center">
  <CSpinner size="sm" variant="grow" style={{ marginTop: "25%", width: '4rem', height: '4rem' }} />
</div>      
      )}
    </>
  );
};

export default Product
