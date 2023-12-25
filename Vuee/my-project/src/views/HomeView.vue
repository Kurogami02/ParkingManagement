<template>
    <v-carousel>
  <v-carousel-item
    src="https://imagev3.vietnamplus.vn/w1000/Uploaded/2023/natmjs/2020_08_16/vnpsan_bay_noi_bai.JPG.webp"
    cover
  ></v-carousel-item>

  <v-carousel-item
    src="https://cafefcdn.com/203337114487263232/2023/10/18/anh-1-1697615190358-1697615192352584196727.jpg"
    cover
  ></v-carousel-item>

  <v-carousel-item
    src="https://static-images.vnncdn.net/files/publish/2023/7/7/lap-quy-hoach-san-bay-thu-2-vung-thu-do-quy-mo-ra-sao-724.jpg"
    cover
  ></v-carousel-item>
</v-carousel>
<div class="space"></div>

            <v-row>
                <v-col md="3" v-for="bx in listBaiXe" :key="bx.ID">
                    <v-card
                        class="mx-auto"
                        max-width="400"
                    >
                        <v-img
                        class="align-end text-white"
                        height="200"
                        :src="'https://localhost:7117/Photos/' + bx.HinhAnh"
                        cover
                        >   
                        </v-img>

                        <v-card-subtitle class="pt-4">
                        {{ bx.ViTri }}
                        
                        </v-card-subtitle>

                        <v-card-subtitle class="pt-4">
                        <p>Chỗ trống: {{ bx.SoLuong }}</p>                        
                        </v-card-subtitle>

                        <v-card-text>
                        
                        <div>{{ bx.TenXe }}</div>
                        </v-card-text>

                        <v-card-actions>

                        <v-btn :to="{ name: 'detail', params: { BaiXeID: bx.ID } }" color="blue">
                        Xem chi tiết
                        </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-col>
            </v-row>
</template>
<style>
  .space {
  margin-bottom: 20px; /* Điều này tạo khoảng cách 20px giữa "Thêm" và "Trangchinh" */
}
  </style>
<script>
import axios from 'axios';
export default {
  data() {
    return {  
      listBaiXe: [],
      errors: [],
      showForm: false,
      TenXe: "",
      ViTri: "",
      SoLuong: "",
      HinhAnh: "",
      DanhMucID: "",
      ID: "",
      message: "",
    };
  },
  created() {
    this.fetchBaiXes();
  },
  methods: {
    fetchBaiXes() {
      axios.get('https://localhost:7117/api/BaiXe/')
        .then(response => {
          this.listBaiXe = response.data;
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
}
}
</script>