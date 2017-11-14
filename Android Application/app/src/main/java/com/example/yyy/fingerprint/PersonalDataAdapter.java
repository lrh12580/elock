package com.example.yyy.fingerprint;

import android.content.Context;
import android.support.annotation.NonNull;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import java.util.List;

/**
 * Created by admin on 2017/10/20.
 */

public class PersonalDataAdapter extends ArrayAdapter<String[]> {

    private int resourceId;
    private TextView shuxing,shuju;


    // 第一个参数是上下文，一般为this。
    // 第二个参数是自定义的布局文件，比如下面的就是R.layout.list_item。
    // 第三个参数是布局中用来显示文字的TextView的id，
    // 第四个参数是数据集合
    public PersonalDataAdapter(Context context, int resource, List<String[]> objects) {
        super(context, resource, objects);
        resourceId=resource;
    }



    // 系统显示列表时，首先实例化一个适配器（这里将实例化自定义的适配器）。
    // 当手动完成适配时，必须手动映射数据，这需要重写getView（）方法。
    // 系统在绘制列表的每一行的时候将调用此方法。
    // getView()有三个参数，
    // position表示将显示的是第几行，
    // covertView是从布局文件中inflate来的布局。
    // 我们用LayoutInflater的方法将定义好的image_item.xml文件提取成View实例用来显示。
    // 然后将xml文件中的各个组件实例化（简单的findViewById()方法）。
    // 这样便可以将数据对应到各个组件上了。
    @NonNull
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View view;
        String[] map = getItem(position);
        //list里有三个string分别存放name(0）time(1) date(2)

        // 获取数据
        if (convertView == null) {
            view = LayoutInflater.from(getContext()).inflate(resourceId, null);
            shuxing = (TextView)view.findViewById(R.id.shuxing);
            shuju = (TextView)view.findViewById(R.id.shuju);
        } else {
            view = convertView;
        }

        if(map!=null) {
            if(shuxing != null) {
                shuxing.setText(map[0]);
                shuju.setText(map[1]);
            }
        }

        return view;

    }
}

